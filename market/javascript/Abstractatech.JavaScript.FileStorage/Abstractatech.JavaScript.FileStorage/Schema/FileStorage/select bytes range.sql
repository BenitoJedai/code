select 

cast(substr(ContentBytes, @ContentBytesRangeOffset /* integer */, @ContentBytesRangeLength /* integer */) as blob) as ContentBytes

from FileStorageTable 
where ContentKey = @ContentKey /* integer */
