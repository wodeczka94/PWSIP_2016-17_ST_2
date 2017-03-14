package com.example.bartosz.aplikacja;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetSocketAddress;
import java.net.Socket;

public class MainActivity extends AppCompatActivity {
    protected EditText editText;
    protected Polaczenie polaczenie;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        editText = (EditText)findViewById(R.id.editText);
        polaczenie = new Polaczenie();
        polaczenie.execute();
    }

    protected void buttonWyslij_onClick(View v){
        polaczenie.wyslijWiadomosc(editText.toString());
        Toast toast = Toast.makeText(getApplicationContext(), "Wiadomość testowa wysłana.", Toast.LENGTH_SHORT);
        toast.show();
    }

    protected class Polaczenie extends AsyncTask<Void, byte[], Void>{
        Socket socket;
        InputStream inputStream;
        OutputStream outputStream;

        @Override
        protected Void doInBackground(Void... parms) {
            try {
                socket = new Socket();
                socket.connect(new InetSocketAddress("10.0.2.2", 1024));

                inputStream = socket.getInputStream();
                outputStream = socket.getOutputStream();

                byte[] buffer = new byte[4096];
                int read = 0;

                while(socket.isConnected()){
                    read = inputStream.read(buffer);

                    if (read != -1){
                        publishProgress(buffer);
                    }
                }
            } catch (IOException e) {
                e.printStackTrace();
            }

            return null;
        }

        @Override
        protected void onProgressUpdate(byte[]... progress) {
            if (progress.length > 0) {
                editText.setText(new String(progress[0]));
            }
        }

        protected void wyslijWiadomosc(final String wiadomosc) {
            if (socket.isConnected()) {
                new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            outputStream.write(wiadomosc.getBytes());
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                }).start();
            }
        }
    }
}