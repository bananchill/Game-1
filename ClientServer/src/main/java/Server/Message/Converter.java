package Server.Message;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.dataformat.xml.XmlMapper;

public class Converter {

    public static String objectToXml(Object object) throws JsonProcessingException {
        XmlMapper xmlMapper = new XmlMapper();
        String xml = xmlMapper.writeValueAsString(object);
        return xml;
    }

    public static Object xmlToObject(String xml, Object obj) throws JsonProcessingException {
        XmlMapper xmlMapper = new XmlMapper();
        Object m = xmlMapper.readValue(xml, obj.getClass());
        return m;
    }
}
