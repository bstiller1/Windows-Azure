using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MyWebRole
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("MyConnectionString"));

            // Create the blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("quicklap");

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, true))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    // Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                    TextBox2.Text = "Block blob of length" + blob.Properties.Length + ": " + blob.Uri;
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                   // Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                    TextBox2.Text = "Page blob of length" + pageBlob.Properties.Length + ": " + pageBlob.Uri;
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                   // Console.WriteLine("Directory: {0}", directory.Uri);
                    TextBox2.Text = "Directory:" + directory.Uri;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Setup the connection to Azure Storage
            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("MyConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            // Get and create the container
            var blobContainer = blobClient.GetContainerReference("quicklap");
            blobContainer.CreateIfNotExists();
            // upload a text blob
            var blob = blobContainer.GetBlockBlobReference(Guid.NewGuid().ToString());
            //byte[] data = new byte[] { 0, 1, 2, 3, 4, 5 };
            //blob.UploadFromByteArray(data, 0, data.Length);
            blob.UploadText(TextBox1.Text);
            // log a message that can be viewed in the diagnostics tables called WADLogsTable
            System.Diagnostics.Trace.WriteLine("Added blob to Azure Storage");
        //Microsoft.WindowsAzure.Storage.Blob.CloudBlockBlob
           // var newBlob = blobContainer.ToString();
            // TextBox2.Text = "Added " + newBlob + " to Azure Storage";
        }
    }
}