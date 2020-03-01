using OpenTK.Graphics.ES30;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V1._0.Engine
{
    public class Loader
    {
        private List<Int32> VertexArrayObjects = new List<Int32>();
        private List<Int32> VertexBufferObjects = new List<Int32>();
        public RawModel LoadToVAO(float[] positions, int[] indices)
        {
            var vertexArrayObject = CreateVertexArrayObject();
            BindIndicesBuffer(indices);
            StoreDataInAttributeList(0, positions);
            UnbindVAO();
            return new RawModel(vertexArrayObject, indices.Length);
        }

        private int CreateVertexArrayObject()
        {
            var vertexArrayObject = GL.GenVertexArray();
            VertexArrayObjects.Add(vertexArrayObject);
            GL.BindVertexArray(vertexArrayObject);
            return vertexArrayObject;
        }

        private void StoreDataInAttributeList(int attributeNumber, float[] data)
        {
            var vertexBufferObject = GL.GenBuffer();
            VertexBufferObjects.Add(vertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeNumber, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
        
        private void BindIndicesBuffer(int[] indices)
        {
            var vertexBufferObject = GL.GenBuffer();
            VertexBufferObjects.Add(vertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
        }

        private void CleanUp()
        {
            foreach(var vertexArrayObject in VertexArrayObjects)
            {
                GL.DeleteVertexArray(vertexArrayObject);
            }
            foreach(var vertexBufferObject in VertexBufferObjects)
            {
                GL.DeleteBuffer(vertexBufferObject);
            }
        }

        private void UnbindVAO()
        {
            GL.BindVertexArray(0);
        }
    }
}
