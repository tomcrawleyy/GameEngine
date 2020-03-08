using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V1._0.Engine.Shaders;

namespace V1._0
{
    public class Game : GameWindow
    {
        readonly float[] _vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };
        int _vertexBufferObject;
        int _vertexArrayObject;

        Shader _shader;

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            _shader = new Shader("..\\..\\Engine\\Shaders\\shader.vert", "..\\..\\Engine\\Shaders\\shader.frag");
            //GL.GetInteger(GetPName.MajorVersion, out int major);
            //GL.GetInteger(GetPName.MinorVersion, out int minor);
            //Console.WriteLine("Major: " + major + " Minor: " + minor);
            _vertexBufferObject = GL.GenBuffer();
            _vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _shader.Use()
;
            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            _shader.Dispose();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(_vertexBufferObject);
            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
    }
}
