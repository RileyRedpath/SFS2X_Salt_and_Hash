package riley.helloworldSFSX;

import com.smartfoxserver.v2.extensions.SFSExtension;
import com.smartfoxserver.v2.components.login.*;
import com.smartfoxserver.v2.db.SFSDBManager;

import java.util.Arrays;


public class MyExtension extends SFSExtension {
	
	private LoginAssistantComponent lac;
	public SFSDBManager DB;
	
	@Override
	public void init() {
		
		lac = new LoginAssistantComponent(this);
		
		this.addRequestHandler("MessageSent", MyExtensionHandler.class);
		this.addRequestHandler("MessageRequest", MessageRequestHandler.class);
        
        lac.getConfig().loginTable = "users";
        lac.getConfig().userNameField = "username";
        lac.getConfig().passwordField = "password";//"hash";
        lac.getConfig().extraFields = Arrays.asList("salt", "message");
        lac.getConfig().customPasswordCheck = true;
        lac.getConfig().preProcessPlugin = new LoginPreProcess();
        //lac.getConfig().postProcessPlugin = new XLoginPostProcess();
        DB = (SFSDBManager) getParentZone().getDBManager();
        
	}
	
	public void destroy(){
		lac.destroy();
		super.destroy();
	}

}
