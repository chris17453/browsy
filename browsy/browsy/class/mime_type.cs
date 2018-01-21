using System;
namespace browsy{
    public class mime_type{ 
        public string extension{ get; set; }
        public string name     { get; set; }
        public string mime     { get; set; }

        public mime_type(string type) {
            this.extension=type;
            switch(type)  {
                case "aac"  : name="AAC audio file";                                     mime="audio/aac";                    break;
                case "abw"  : name="AbiWord document";                                   mime="application/x-abiword";        break;
                case "arc"  : name="Archive document";                                   mime="application/octet-stream";     break;
                case "avi"  : name="AVI: Audio Video Interleave";                        mime="video/x-msvideo";              break;
                case "azw"  : name="Amazon Kindle eBook format";                         mime="application/vndcase";          break;
                case "bin"  : name="Any kind of binary data";                            mime="application/octet-stream";     break;
                case "bz"   : name="BZip archive";                                       mime="application/x-bzip";           break;
                case "bz2"  : name="BZip2 archive";                                      mime="application/x-bzip2";          break;
                case "csh"  : name="C-Shell script";                                     mime="application/x-csh";            break;
                case "css"  : name="Cascading Style Sheets (CSS)";                       mime="text/css";                     break;
                case "csv"  : name="Comma-separated values (CSV)";                       mime="text/csv";                     break;
                case "doc"  : name="Microsoft Word";                                     mime="application/msword";           break;
                case "docx" : name="Microsoft Word (OpenXML)";                           mime="application/vndcase";          break;
                case "eot"  : name="MS Embedded OpenType fonts";                         mime="application/vndcase";          break;
                case "epub" : name="Electronic publication (EPUB)";                      mime="application/epub+zip";         break;
                case "gif"  : name="Graphics Interchange Format (GIF)";                  mime="image/gif";                    break;
                case "htm"  : name="HyperText Markup Language (HTML)";                   mime="text/html";                    break;
                case "html" : name="HyperText Markup Language (HTML)";                   mime="text/html";                    break;
                case "ico"  : name="Icon format";                                        mime="image/x-icon";                 break;
                case "ics"  : name="iCalendar format";                                   mime="text/calendar";                break;
                case "jar"  : name="Java Archive (JAR)";                                 mime="application/java-archive";     break;
                case "jpeg" : name="JPEG images";                                        mime="image/jpeg";                   break;
                case "jpg"  : name="JPEG images";                                        mime="image/jpeg";                   break;
                case "js"   : name="JavaScript (ECMAScript)";                            mime="application/javascript";       break;
                case "json" : name="JSON format";                                        mime="application/json";             break;
                case "mid"  : name="Musical Instrument Digital Interface (MIDI)";        mime="audio/midi";                   break;
                case "midi" : name="Musical Instrument Digital Interface (MIDI)";        mime="audio/midi";                   break;
                case "mpeg" : name="MPEG Video";                                         mime="video/mpeg";                   break;
                case "mpkg" : name="Apple Installer Package";                            mime="application/vndcase";          break;
                case "odp"  : name="OpenDocument presentation document";                 mime="application/vndcase";          break;
                case "ods"  : name="OpenDocument spreadsheet document";                  mime="application/vndcase";          break;
                case "odt"  : name="OpenDocument text document";                         mime="application/vndcase";          break;
                case "oga"  : name="OGG audio";                                          mime="audio/ogg";                    break;
                case "ogv"  : name="OGG video";                                          mime="video/ogg";                    break;
                case "ogx"  : name="OGG";                                                mime="application/ogg";              break;
                case "otf"  : name="OpenType font";                                      mime="font/otf";                     break;
                case "png"  : name="Portable Network Graphics";                          mime="image/png";                    break;
                case "pdf"  : name="Adobe Portable Document Format (PDF)";               mime="application/pdf";              break;
                case "ppt"  : name="Microsoft PowerPoint";                               mime="application/vndcase";          break;
                case "pptx" : name="Microsoft PowerPoint (OpenXML)";                     mime="application/vndcase";          break;
                case "rar"  : name="RAR archive";                                        mime="application/x-rar-compressed"; break;
                case "rtf"  : name="Rich Text Format (RTF)";                             mime="application/rtf";              break;
                case "sh"   : name="Bourne shell script";                                mime="application/x-sh";             break;
                case "svg"  : name="Scalable Vector Graphics (SVG)";                     mime="image/svg+xml";                break;
                case "swf"  : name="Small web format (SWF) or Adobe Flash document  a";  mime="pplication/x-shockwave-flash"; break;
                case "tar"  : name="Tape Archive (TAR)";                                 mime="application/x-tar";            break;
                case "tif"  : name="Tagged Image File Format (TIFF)";                    mime="image/tiff";                   break;
                case "tiff" : name="Tagged Image File Format (TIFF)";                    mime="image/tiff";                   break;
                case "ts"   : name="Typescript file";                                    mime="application/types";            break;
                case "ttf"  : name="TrueType Font";                                      mime="font/ttf";                     break;
                case "vsd"  : name="Microsoft Visio";                                    mime="application/vndcase";          break;
                case "wav"  : name="Waveform Audio Format";                              mime="audio/x-wav";                  break;
                case "weba" : name="WEBM audio";                                         mime="audio/webm";                   break;
                case "webm" : name="WEBM video";                                         mime="video/webm";                   break;
                case "webp" : name="WEBP image";                                         mime="image/webp";                   break;
                case "woff" : name="Web Open Font Format (WOFF)";                        mime="font/woff";                    break;
                case "woff2": name="Web Open Font Format (WOFF)";                        mime="font/woff2";                   break;
                case "xhtml": name="XHTML";                                              mime="application/xhtml+xml";        break;
                case "xls"  : name="Microsoft Excel";                                    mime="application/vndcase";          break;
                case "xlsx" : name="Microsoft Excel (OpenXML)";                          mime="application/vndcase";          break;
                case "xml"  : name="XML";                                                mime="application/xml";              break;
                case "xul"  : name="XUL";                                                mime="application/vndcase";          break;
                case "zip"  : name="ZIP archive";                                        mime="application/zip";              break;
                case "3gp"  : name="3GPP";                                               mime="audio/video container";        break;
                case "3g2"  : name="3GPP2";                                              mime="audio/video containereak";     break;
                case "7z"   : name="7-zip archive";                                      mime="application/x-7z-compressed";  break;
                default     : name="Unknown";                                            mime="text/plain";                   break;
            }
        }
    }
}
