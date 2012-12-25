select coalesce(max(id), 0) as id /* integer */ 
from  SystemConsoleOut
where session = @session /* integer */ 