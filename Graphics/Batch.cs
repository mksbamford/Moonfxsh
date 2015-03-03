﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace Moonfish.Graphics
{
    public class Batch : IDisposable
    {
        public int VertexArrayObjectIdent { get { return vao; } }
        public IList<int> BufferIdents { get { return buffers; } }

        bool disposed = false;

        int vao;
        List<int> buffers;

        public Batch( int vertexArrayObjectident, IEnumerable<int> inBuffers )
        {
            vao = vertexArrayObjectident;
            buffers = new List<int>( inBuffers );
        }

        public Batch( )
        {
            buffers = new List<int>();
            vao = GL.GenVertexArray();
        }

        public class Handle : IDisposable
        {
            const int @default = 0;

            public Handle( int ident )
            {
                GL.BindVertexArray( ident );
            }

            public void Dispose( )
            {
                GL.BindVertexArray( @default );
            }
        }

        public IDisposable Begin( ) { return new Handle( this.VertexArrayObjectIdent ); }

        public void VertexAttribArray( int index, int count, VertexAttribPointerType type, bool normalised = false, int stride = 0, int offset = 0 )
        {
            GL.VertexAttribPointer( index, count, type, normalised, stride, offset );
            GL.EnableVertexAttribArray( index );
#if DEBUG
            OpenGL.ReportError();
#endif
        }

        public void GenerateBuffer( )
        {
            var buffer = GL.GenBuffer();
#if DEBUG
            OpenGL.ReportError();
#endif
            buffers.Add( buffer );
        }

        public void BindBuffer( BufferTarget target, int buffer )
        {
            GL.BindBuffer( target, buffer );
#if DEBUG
            OpenGL.ReportError();
#endif
        }

        public void BufferVertexAttributeData<T>( T[] data ) where T : struct
        {
            var dataSize = ( IntPtr )( System.Runtime.InteropServices.Marshal.SizeOf( typeof( T ) ) * data.Length );
            GL.BufferData<T>( BufferTarget.ArrayBuffer, dataSize, data, BufferUsageHint.DynamicDraw );

#if DEBUG
            OpenGL.ReportError();
#endif
        }

        public void BufferElementArrayData( ushort[] indices )
        {
            GL.BufferData( BufferTarget.ElementArrayBuffer, ( IntPtr )( indices.Length * sizeof( ushort ) ), indices, BufferUsageHint.DynamicDraw );

#if DEBUG
            OpenGL.ReportError();
#endif
        }

        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposed ) return;
            if ( disposing )
            {
            GL.DeleteVertexArray( vao );
            GL.DeleteBuffers( buffers.Count, buffers.ToArray() );
#if DEBUG
            OpenGL.ReportError();
#endif
            }
            disposed = true;
        }
    }
}
