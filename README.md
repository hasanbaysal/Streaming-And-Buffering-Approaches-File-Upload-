# AspNetCore FileUpload Approaches


two methods are used to upload files

### buffering 
 * buffering method is better approach for small files
 * not recommended for large files because it allocates server resources at once


### streaming
 * buffering method is better approach for large files
 * data comes in a stream
 * you cannot implicit model binding with streming aproach
 
 
 
- [x] i will add an exmaple for implicit model binding with streaming aproach 


#### I added the codes on 20.04.2023
In the streaming method, the http request comes as a multi-part
In these parts, you can separate your data according to the boundary value.
and you can make model binding by reading its data with stream method


![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/1.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/2.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/3.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/31png.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/4.png)
