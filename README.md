# AspNetCore FileUpload Approaches


two methods are used to upload files

### buffering 
 * buffering method is better approach for small files
 * not recommended for large files because it allocates server resources at once


### streaming
 * buffering method is better approach for large files
 * data comes in a stream
 * you cannot implicit model binding with streming aproach
 
 
 
 i will add an exmaple for implicit model binding with streaming aproach 





![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/1.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/2.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/3.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/31.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/4.png)
