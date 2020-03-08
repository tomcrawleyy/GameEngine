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
             0.5f,  0.5f, 0.0f, // top right
             0.5f, -0.5f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, // top left
        };

        readonly uint[] indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3  // second triangle
        };
        int _vertexBufferObject;
        int _vertexArrayObject;
        int _elementBufferObject;

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
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            _shader = new Shader("..\\..\\Engine\\Shaders\\shader.vert", "..\\..\\Engine\\Shaders\\shader.frag");
            _shader.Use();

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

;
            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            _shader.Dispose();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteBuffer(_elementBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteProgram(_shader.Handle);
            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, new IntPtr(0));
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
