using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V1._0.Engine
{
    public class RawModel
    {
        public int VertexArrayObject { get; private set; }
        public int VertexCount { get; private set; }
        public RawModel(int vertexArrayObject, int vertexCount) {
            this.VertexArrayObject = vertexArrayObject;
            this.VertexCount = vertexCount;
        }
    }
}
