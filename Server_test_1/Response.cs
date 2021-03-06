﻿using System;
using System.IO;
using System.Net.Sockets;

namespace Server_test_1
{
    public class Response
    {

        private Byte[] data = null;
        private String status;
        private String mime;



        private Response(String status, String mime, Byte[] data)
        {
            this.status = status;
            this.mime = mime;
            this.data = data;
        }

        public static Response From (Request request)
        {
            if (request == null)
                return MakeNullRequest();
            if (request.Type == "GET")
            {
                String file = Environment.CurrentDirectory + HTTPServer.WEB_DIR + request.URL;
                FileInfo f = new FileInfo(file);


                if (f.Exists && f.Extension.Contains("."))
                {
                    return MakeFromFile(f);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(f + "/");
                    if (!di.Exists)
                        return MakePageNotFound();

                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo ff in files)
                    {
                        String n = ff.Name;
                        if (n.Contains("default.html") || n.Contains("default.htm") || n.Contains("index.htm") || n.Contains("index.html"))
                        {
                            f = ff;
                            return MakeFromFile(ff);
                        }
                    }
                }
            }
            else
                return MakeMethodNotAllowed();
                return MakeMethodNotAllowed();
            
        }

        private static Response MakeFromFile(FileInfo f)
        {

            FileStream fileStream = f.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, d.Length);
            fileStream.Close();
            return new Response("200 OK", "text/html", d);
        }

        private static Response MakeMethodNotAllowed()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "405.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, d.Length);
            fileStream.Close();
            return new Response("405 Method not allowed", "text/html", d);
        }

        private static Response MakeNullRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "400.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, d.Length);
            fileStream.Close();
            return new Response("400 Bad Request", "text/html", d);
        }

        private static Response MakePageNotFound()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "404.html";
            FileInfo fileInfo = new FileInfo(file);
            FileStream fileStream = fileInfo.OpenRead();
            BinaryReader reader = new BinaryReader(fileStream);
            Byte[] d = new Byte[fileStream.Length];
            reader.Read(d, 0, d.Length);
            fileStream.Close();
            return new Response("400 Page Not Found", "text/html", d);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(String.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n",
                HTTPServer.VERSION, status, HTTPServer.NAME, mime, data.Length));
            stream.Write(data, 0, data.Length);
        }
    }
}
