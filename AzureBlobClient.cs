using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

public class AzureBlobClient {  
    private static string _connectionString = "DefaultEndpointsProtocol=https;AccountName=mihailaccount;AccountKey=540ImEfQ+A5u+7gVsWN/FZaGkOW96QPZG4Q7MkFobzUr8QgsTTpuBMejuhkFXJHxQmxeDWlwWuBp+AStCUgPZA==;EndpointSuffix=core.windows.net";
    private static string _containerName = "files";  

    private static BlobServiceClient _serviceClient = new BlobServiceClient(_connectionString); 
    private static BlobContainerClient _containerClient = _serviceClient.GetBlobContainerClient(_containerName);
    public static async Task UploadBlob(string path, string fileName)
    {
        var localFile = Path.Combine(path, fileName);  
        var blobClient = _containerClient.GetBlobClient(fileName);  
        Console.WriteLine("Uploading to Blob storage");  
        using FileStream uploadFileStream = File.OpenRead(localFile);  
        await blobClient.UploadAsync(uploadFileStream, true);  
        uploadFileStream.Close();  
    }
    
    public static async Task ListBlob()
    {
        Console.WriteLine("Items list:");
        await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
        {
            Console.WriteLine("\t" + blobItem.Name);
        }
    }
    
    public static async Task DeleteBlob(string fileName)
    {
        Console.WriteLine("Delete file");
        _containerClient.DeleteBlobIfExists(fileName);
    }
    
    public static async Task DowloadBlob(string path, string fileName)
    {
        var localFile = Path.Combine(path, fileName); 
        string downloadFilePath = localFile.Replace(".txt", "DOWNLOADED.txt");
        Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

        await _containerClient.GetBlobClient(fileName).DownloadToAsync(downloadFilePath);
    }
}