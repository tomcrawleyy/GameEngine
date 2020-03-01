using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V1._0.Engine
{
    public class Renderer
    {

        public void Prepare()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        public void Render(RawModel model)
        {
            GL.BindVertexArray(model.VertexArrayObject);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, model.VertexCount);
            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }
    }
}
