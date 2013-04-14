select 

ContentKey, ContentValue, ContentType,
length(ContentBytes) as ContentBytesLength

from FileStorageTable

order by lower(ContentValue)