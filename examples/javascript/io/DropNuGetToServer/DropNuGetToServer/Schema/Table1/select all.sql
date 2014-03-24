select ContentKey, ContentValue, 
length(ContentBytes) as ContentBytesLength,
  coalesce((select MemberValue from Table1Meta where MemberName = 'Left' and DeclaringType = ContentKey order by MetaKey desc), '0') as Left,
  coalesce((select MemberValue from Table1Meta where MemberName = 'Top' and DeclaringType = ContentKey order by MetaKey desc), '0') as Top,
  coalesce((select MemberValue from Table1Meta where MemberName = 'Width' and DeclaringType = ContentKey order by MetaKey desc), '0') as Width,
  coalesce((select MemberValue from Table1Meta where MemberName = 'Height' and DeclaringType = ContentKey order by MetaKey desc), '0') as Height
 from Table1