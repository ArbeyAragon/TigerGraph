using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module
{
    private static Module _instance;
    private static AuthService _authService;
    private static MicroService _microService;
    private static CommunicationService _communicationService;
    
    private Main _main;
    private Module(Main main) { 
        this._main = main;
        _authService = new AuthService(_main);
        _microService = new MicroService(_authService, _main);
        _communicationService = new CommunicationService(_authService, 
                                                        _microService, 
                                                        _main);
    }

    public static Module GetInstance(Main main)
    {
        if (_instance == null)
        {
            _instance = new Module(main);
        }
        return _instance;
    }

    public AuthService AuthService(){
        return _authService;
    }

    public MicroService MicroService(){
        return _microService;
    }

    public CommunicationService CommunicationService(){
        return _communicationService;
    }
}
