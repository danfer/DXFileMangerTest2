using Amazon.S3.Model;
using Amazon.S3;
using System.Collections.Generic;
using DevExpress.Web;

namespace DXFileMangerTest2.Services
{
    public class AwsS3FileProvider : FileSystemProviderBase
    {
        private readonly string bucketName;
        private readonly AmazonS3Client s3Client;

        public AwsS3FileProvider(string rootFolder) : base(rootFolder)
        {
            var awsAccessKey = "SOME"; //ConfigurationManager.AppSettings["AWSAccessKey"];
            var awsSecretKey = "some"; //ConfigurationManager.AppSettings["AWSSecretKey"];
            this.bucketName = "somesystemsdocumentmanagement"; //ConfigurationManager.AppSettings["AWSBucketName"];
            this.s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, Amazon.RegionEndpoint.USEast1); // Change to your region
        }

        public override IEnumerable<FileManagerFile> GetFiles(FileManagerFolder folder)
        {
            var files = new List<FileManagerFile>();
            var request = new ListObjectsV2Request
            {
                BucketName = this.bucketName,
                Prefix = folder.FullName
            };
            var response = s3Client.ListObjectsV2(request);
            foreach (var s3Object in response.S3Objects)
            {
                var file = new FileManagerFile(this, folder, s3Object.Key);
                files.Add(file);
            }
            return files;
        }

        public override IEnumerable<FileManagerFolder> GetFolders(FileManagerFolder parentFolder)
        {
            var folders = new List<FileManagerFolder>();
            var request = new ListObjectsV2Request
            {
                BucketName = bucketName,
                Prefix = parentFolder.FullName
            };
            var response = s3Client.ListObjectsV2(request);
            foreach (var s3Object in response.S3Objects)
            {
                var folder = new FileManagerFolder(this, parentFolder, s3Object.Key);
                folders.Add(folder);
            }
            return folders;
        }
    }
}