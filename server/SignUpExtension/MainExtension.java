package com.riley.helloworldSFS2X.SignUpExtension;

import com.smartfoxserver.v2.components.signup.SignUpAssistantComponent;
import com.smartfoxserver.v2.extensions.SFSExtension;
import java.util.Arrays;

/**
 * Created by Riley on 2017-02-09.
 */
public class MainExtension extends SFSExtension{

    private SignUpAssistantComponent suac;

    @Override
    public void init(){
        this.suac = new SignUpAssistantComponent();
        this.addRequestHandler(SignUpAssistantComponent.COMMAND_PREFIX, suac);
        suac.getConfig().isEmailRequired = false;
        suac.getConfig().extraFields = Arrays.asList("salt","message");
        suac.getConfig().preProcessPlugin = new SignUpPreProcess();
    }
}
