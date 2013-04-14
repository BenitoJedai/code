select ContentKey, ContentValue, ContentType from FileStorageTable

order by lower(ContentValue)