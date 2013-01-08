select id, message
from PointerSync

where 
    id > @FromTransaction /* integer */
and id <= @ToTransaction /* integer */

order by id