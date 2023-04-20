# AspNetCore FileUpload Approaches


two methods are used to upload files

## buffering 
 * buffering method is better approach for small files
 * not recommended for large files because it allocates server resources at once
 * When working with large files, your system may have problems because it will use a lot of ram memory of your server.
 *  You don't need to do anything extra for this method. If you are using IFormfile object you are actually using this method
 * this method first buffers the data in ram space then writes it to physical file
 
 


## streaming
 * buffering method is better approach for large files
 * you cannot implicit model binding with streming aproach
 *  this approach reduces ram usage
 * In this method, instead of buffering the data in memory, it minimizes ram usage by writing the data coming in the stream to the file.
 * if you are going to get other data from the form along with the file, you have to make explicit modelbinding
 
 
 
 
- [x] i will add an exmaple for implicit model binding with streaming aproach 


#### I added the codes on 20.04.2023

in the streaming approach you can retrieve the file you want to upload from the sections of the incoming http request that are split into parts if you also post data other than file information such as the product name within the same request you can create a stream under the relevant section to read this data and then perform an explicit model binding process.









![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/1.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/2.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/3.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/31png.png)
![uml](https://github.com/hasanbaysal/Streaming-And-Buffering-Approaches-File-Upload-/blob/master/ss/4.png)
