package FirstGame;

public class NewPlayer {
    private String nickname;
    private String mail;
    private String password;

    public NewPlayer(String nickname, String mail, String password) {
        this.nickname = nickname;
        this.mail = mail;
        this.password = password;
    }

    public String getNickname() {
        return nickname;
    }

    public void setNickname(String nickname) {
        this.nickname = nickname;
    }

    public String getMail() {
        return mail;
    }

    public void setMail(String mail) {
        this.mail = mail;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
