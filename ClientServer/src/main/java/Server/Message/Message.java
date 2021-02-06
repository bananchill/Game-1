package Server.Message;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

import java.io.Serializable;

@JacksonXmlRootElement(localName = "Message")
@JsonInclude(JsonInclude.Include.NON_EMPTY)
public class Message implements Serializable {

    @JacksonXmlProperty(localName = "type")
    private MessageType type;
    @JacksonXmlProperty(localName = "data")
    private String data;
    @JsonIgnore
    private String xml;

    public Message() { }

    public Message(MessageType type) throws JsonProcessingException {
        this.type = type;
        this.data = null;
        setXml();
    }

    public Message(MessageType type, String data) throws JsonProcessingException {
        this.type = type;
        this.data = data;
        setXml();
    }

    public MessageType getType() {
        return type;
    }

    public String getData() {
        return data;
    }

    public String getXml() {
        return xml;
    }

    public void setXml() {
        try {
            this.xml = Converter.objectToXml(this);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
        }
    }
}