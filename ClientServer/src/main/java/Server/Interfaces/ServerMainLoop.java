package Server.Interfaces;

import Server.Client.Client;

import java.io.IOException;

public interface ServerMainLoop {
    void serverMainLoop(Client client) throws IOException;
}
