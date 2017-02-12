package riley.helloworldSFSX;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.*;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

import java.sql.SQLException;

import com.smartfoxserver.v2.db.SFSDBManager;

public class MyExtensionHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User user, ISFSObject params) {
		String message = params.getUtfString("message");
		String sql = "UPDATE users SET message = ? WHERE username = ?";
		SFSDBManager DB = ((MyExtension)getParentExtension()).DB;
		ISFSObject paramsOut = new SFSObject();
		try {
			DB.executeUpdate(sql, new Object[]{message, user.getName()});
			paramsOut.putBool("success", true);
			paramsOut.putUtfString("message", message);
		} catch (SQLException e) {
			e.printStackTrace();
			paramsOut.putBool("success", false);
		}
		send("MessageSent",paramsOut,user);
	}

}
