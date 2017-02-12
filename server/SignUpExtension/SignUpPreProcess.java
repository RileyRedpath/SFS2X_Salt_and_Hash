package com.riley.helloworldSFS2X.SignUpExtension;

import com.smartfoxserver.v2.components.signup.ISignUpAssistantPlugin;
import com.smartfoxserver.v2.components.signup.SignUpConfiguration;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.*;
import com.smartfoxserver.v2.util.MD5;
import java.security.SecureRandom;
import java.math.BigInteger;

/**
 * Created by Riley on 2017-02-09.
 */
public class SignUpPreProcess implements ISignUpAssistantPlugin{

    @Override
    public void execute(User user, ISFSObject paramsIn, SignUpConfiguration config){
		String password = paramsIn.getUtfString("password");
		//generate salt
		String salt = new BigInteger(128,new SecureRandom()).toString(32);
		//Hash
		String hash = MD5.getInstance().getHash(password + salt);
		//Store
        paramsIn.putUtfString("password",hash);
        paramsIn.putUtfString("salt",salt);
        paramsIn.putUtfString("message","");

    }
}
