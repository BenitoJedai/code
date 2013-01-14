select MyAccountToken.id, MyAccountToken.email 
from MySessionToken, MyAccountToken 
where  MySessionToken.account = MyAccountToken.id
and  MySessionToken.cookie = @cookie /* text */
	

