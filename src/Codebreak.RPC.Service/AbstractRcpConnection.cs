﻿using System;
using Codebreak.Framework.IO;
using Codebreak.Framework.Network;

namespace Codebreak.RPC.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessageBuilder"></typeparam>
    public abstract class AbstractRcpConnection<TMessageBuilder> : AbstractSocketClient
        where TMessageBuilder : RpcMessageBuilder, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public event Action<AbstractRcpMessage> OnMessageEvent;
        
        /// <summary>
        /// 
        /// </summary>
        public RpcMessageBuilder MessageBuilder
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        private int m_messageId;
        private int m_messageLength;
        private readonly BinaryQueue m_messageData;

        /// <summary>
        /// 
        /// </summary>
        protected AbstractRcpConnection()
        {
            MessageBuilder = new TMessageBuilder();

            m_messageId = -1;
            m_messageLength = -1;
            m_messageData = new BinaryQueue();

            OnMessageEvent += OnMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        ~AbstractRcpConnection()
        {
            OnMessageEvent = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Send(AbstractRcpMessage message)
        {
            message.Serialize();
            Send(message.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        protected override void OnBytesRead(byte[] buffer, int offset, int length)
        {
            for (var i = offset; i < offset + length; i++)
            {
                m_messageData.WriteByte(buffer[i]);
            }

            do
            {
                if (m_messageLength == -1 && m_messageData.Count > 3)
                {
                    m_messageLength = m_messageData.ReadInt();
                }
                if (m_messageLength != -1 && m_messageId == -1 && m_messageData.Count > 3)
                {
                    m_messageId = m_messageData.ReadInt();
                }
                if (m_messageLength != -1 && m_messageId != -1 && m_messageData.Count >= m_messageLength)
                {
                    var message = MessageBuilder.BuildMessage(m_messageId, m_messageData.ReadBytes(m_messageLength));

                    OnMessageEvent?.Invoke(message);

                    m_messageId = -1;
                    m_messageLength = -1;
                }
            }
            while ((m_messageLength == -1 || m_messageId == -1) && m_messageData.Count > 3);
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract override void OnConnected();

        /// <summary>
        /// 
        /// </summary>
        protected abstract override void OnDisconnected();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected abstract void OnMessage(AbstractRcpMessage message);
    }
}
