select 
length(ContentBytes) as ContentBytesLength,
ContentValue, ContentType from FileStorageTable where ContentKey = @ContentKey /* integer */
