package riley.helloworldSFSX;

import com.smartfoxserver.v2.components.login.ILoginAssistantPlugin;
import com.smartfoxserver.v2.util.MD5;
import com.smartfoxserver.v2.components.login.LoginData;
import com.smartfoxserver.v2.components.login.PasswordCheckException;

public class LoginPreProcess implements ILoginAssistantPlugin {

	@Override
    public void execute(LoginData ld)
    {
        String clientPass = ld.clientIncomingData.getUtfString("rawPassword");
        // Salt then hash the user's password 
        String hash = MD5.getInstance().getHash(clientPass + ld.extraFields.getUtfString("salt"));
        // Check if the incoming hash matches the hash in the DB
        if (!ld.password.equals(hash)){
            throw new PasswordCheckException();
        }
        // Success!
    }

}
