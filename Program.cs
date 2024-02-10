string path = @"";  
string fileName = "Testfile.txt"; 

await AzureBlobClient.UploadBlob(path, fileName);
await AzureBlobClient.ListBlob();
await AzureBlobClient.DowloadBlob(path, fileName);
await AzureBlobClient.DeleteBlob(fileName);
Console.ReadKey();