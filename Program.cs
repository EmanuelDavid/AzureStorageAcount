﻿using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccountOperations
{
    class Programd
    {
        static void Main(string[] args)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("queue");

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            ActionDescription sendMailTo = new ActionDescription { Id = 3, Email = "da1vid@yahoo.com"};

            // Create a message and add it to the queue.
            CloudQueueMessage jsonMessage = new CloudQueueMessage(JsonConvert.SerializeObject(sendMailTo));
            queue.AddMessage(jsonMessage);
        }
    }

    internal class ActionDescription
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
