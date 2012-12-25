select id, value 
from SystemConsoleOut
where session = @session /* integer */
and id > @id /* integer */
and id <= @nextid /* integer */

         