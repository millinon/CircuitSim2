using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CircuitSim2.Chips;

namespace CircuitSim2
{
    public class Catalog
    {
        public readonly string Name;

        private readonly List<Catalog> SubCats;
        public IEnumerable<Catalog> SubCategories => SubCats;

        private readonly List<Type> Chips;
        public IEnumerable<Type> ChipTypes => Chips;

        public Catalog(string Name, IEnumerable<Catalog> SubCats, IEnumerable<Type> Chips)
        {
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));
            this.SubCats = new List<Catalog>(SubCats);
            this.Chips = new List<Type>(Chips);
        }

        public static Catalog Search(string Name, string NamespacePrefix, Assembly Assembly)
        {
            var assembly_types = Assembly.GetTypes()
                .Where(type => type.Namespace.StartsWith(NamespacePrefix) && type.IsSealed && type.IsSubclassOf(typeof(ChipBase)))
                .Where(type => type != typeof(MetaChip));
            var ns_toks = NamespacePrefix.Split('.');
            var next_namespaces = assembly_types.Select(t => t.Namespace)
                .Where(ns => ns.Split('.').Length > ns_toks.Length)
                .Select(ns => string.Join(".", ns.Split('.').Take(ns_toks.Length + 1)))
                .Distinct();

            return new Catalog(Name,
                next_namespaces.OrderBy(ns => ns)
                    .Select(ns => Search(ns.Split('.')[ns_toks.Length], ns, Assembly)),
                assembly_types.Where(type => type.Namespace == NamespacePrefix)
                    .OrderBy(type => type.Name));
        }

        public static Catalog Default()
        {
            return Search("Default", "CircuitSim2.Chips", Assembly.GetExecutingAssembly());
        }
    }
}
