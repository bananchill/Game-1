package MenuAndChat;

import MenuAndChat.Message.Message;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.dataformat.xml.XmlMapper;

public class Converter {

    public static String messageToXml(Message message) throws JsonProcessingException {//don't use!!!
        XmlMapper xmlMapper = new XmlMapper();
        String xml = xmlMapper.writeValueAsString(message);
        return xml;
    }

    public static Message xmlToMessage(String xml) throws JsonProcessingException {
        XmlMapper xmlMapper = new XmlMapper();
        Message value = xmlMapper.readValue(xml, Message.class);
        return value;
    }
}
