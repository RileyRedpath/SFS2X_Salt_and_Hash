package riley.helloworldSFSX;

import java.sql.SQLException;

import com.smartfoxserver.v2.db.SFSDBManager;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.*;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class MessageRequestHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User user, ISFSObject params) {
		String sql = "SELECT message FROM users WHERE username = ?";
		SFSDBManager DB = ((MyExtension)getParentExtension()).DB;
		ISFSObject paramsOut = new SFSObject();
		try {
			ISFSArray result = DB.executeQuery(sql, new Object[]{user.getName()});
			paramsOut.putBool("success", true);
			String message = (result.getSFSObject(0)).getUtfString("message");
			paramsOut.putUtfString("message", message);
		} catch (SQLException e) {
			e.printStackTrace();
			paramsOut.putBool("success", false);
		}
		send("MessageRequest",paramsOut,user);

	}

}
