﻿
namespace EAuction.Shared.Interface
{
    public interface IRabbitMqProducer
    {
        /// <summary>
        /// 
        /// </summary>
        void Receive();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Publish(string message);
    }
}
