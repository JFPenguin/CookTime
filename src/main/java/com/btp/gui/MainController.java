package com.btp.gui;

import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.scene.control.TextArea;
import java.io.OutputStream;
import java.io.PrintStream;

public class MainController {

    @FXML
    private TextArea console;
    private PrintStream ps ;

    public void initialize() {
        ps = new PrintStream(new Console(console)) ;
    }

    public class Console extends OutputStream {
        private TextArea console;

        public Console(TextArea console) {
            this.console = console;
        }

        public void appendText(String valueOf) {
            Platform.runLater(() -> console.appendText(valueOf));
        }

        public void write(int b) {
            appendText(String.valueOf((char)b));
        }
    }
}
