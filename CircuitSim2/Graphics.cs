using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitSim2
{
    public struct SizeVec
    {
        /// <summary>X dimension</summary>
        public double Length;
        /// <summary>Y dimension</summary>
        public double Width;
        /// <summary>Z dimension</summary>
        public double Height;
    };

    public struct PositionVec
    {
        public double X;
        public double Y;
        public double Z;

        public PositionVec Add(PositionVec rhs)
        {
            return new PositionVec
            {
                X = this.X + rhs.X,
                Y = this.Y + rhs.Y,
                Z = this.Z + rhs.Z,
            };
        }

        public PositionVec Multiply(double[][] Matrix)
        {
            if (Matrix == null)
            {
                throw new ArgumentNullException(nameof(Matrix));
            }
            if (Matrix.Length != 3 || Matrix.Any(row => row.Length != 3))
            {
                throw new InvalidOperationException();
            }

            return new PositionVec
            {
                X = this.X * Matrix[0][0] + this.Y * Matrix[0][1] + this.Z * Matrix[0][2],
                Y = this.X * Matrix[1][0] + this.Y * Matrix[1][1] + this.Z * Matrix[1][2],
                Z = this.X * Matrix[2][0] + this.Y * Matrix[2][1] + this.Z * Matrix[2][2],
            };
        }
    }

    public struct RotationVec
    {
        /// <summary>Rotation about the X-axis in radians</summary>
        public double Alpha;
        /// <summary>Rotation about the Y-axis in radians</summary>
        public double Beta;
        /// <summary>Rotation about the Z-axis in radians</summary>
        public double Gamma;
    }
}
