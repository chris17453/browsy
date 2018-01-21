using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace browsy.model{
    public class directory{
            public string                 path          {get; set; }
            public List<file_system_item> directories   { get; set; }
            public List<file_system_item> files         { get; set; }

            
            public string get_os(){
                OperatingSystem os = Environment.OSVersion;
                PlatformID     pid = os.Platform;
                string os_string=String.Empty;
                switch(pid){
                    case PlatformID.Win32NT     :
                    case PlatformID.Win32S      :
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE       : os_string="Windows"; break;
                    case PlatformID.Unix        : os_string="Linux";   break;
                    default                     : os_string="OSX";     break; 
                }
                return os_string;
            }

            public string get_home_dir(){
                string home_dir=null;
                string os=get_os();

                switch(os){
                    case "Linux"    :   home_dir = System.Environment.GetEnvironmentVariable("HOME");
                        break;

                    case "Windows"  :   string home_drive;
                        home_drive=System.Environment.GetEnvironmentVariable("HOMEDRIVE");
                        home_dir  = System.Environment.GetEnvironmentVariable("HOMEPATH");
                        home_dir  =home_drive+@"\"+home_dir;
                        break;
                }
                return home_dir;
            }

           public static string get_file_size(string file){
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                if (fileInfo.Exists){ 
                    return  core.get_bytes_readable(fileInfo.Length);
                }
                return "-";
            }

            static List<file_system_item> get_directories(string path){
                List<file_system_item> t_item=new List<file_system_item>();
                try{

                    List<string> t_dir=Directory.GetDirectories(path).OrderBy(f => f).ToList();
                    foreach(string t in t_dir) {
                        file_system_item tf=new file_system_item();
                        DirectoryInfo dir = new DirectoryInfo(t);
                        if ((dir.Attributes & FileAttributes.Hidden) == (FileAttributes.Hidden)) {  
                            tf.hidden=true;
                        }
                        DateTime dt = Directory.GetLastWriteTime(t);
                        tf.type="folder";
                        tf.path=t;
                        tf.modified=core.relative_time(dt);
                        string[] tokens=t.Split(new char[] {'/','\\'});
                        tf.name=tokens.Last();
                        t_item.Add(tf);
                    }

                }catch (UnauthorizedAccessException){

                }
                return t_item;
            }
          
        static List<file_system_item> get_files(dir_path path){
                List<file_system_item> t_item=new List<file_system_item>();
                try{
                    var sorted = Directory.GetFiles(".").OrderBy(f => f);
                List<string> t_files=Directory.GetFiles(path.path,"*",SearchOption.TopDirectoryOnly)
                      .Where((string x) => 
                        {
                    if(null==path.filters || !path.filters.Any())  return true;
                            foreach (string z in  path.filters) {
                        if(x.EndsWith(z,StringComparison.CurrentCulture)) return true;
                                } return false;
                       }).OrderBy(f => f).ToList();
                    foreach(string t in t_files) {
                        file_system_item tf=new file_system_item();
                        var fInfo = new FileInfo(t);
                        if (fInfo.Attributes.HasFlag(FileAttributes.Hidden)){
                            tf.hidden=true;
                        }
                        DateTime dt = File.GetLastWriteTime(t);

                        tf.type="file";
                        tf.path=t;
                        tf.size=get_file_size(t);
                        tf.modified=core.relative_time(dt);
                        tf.name=Path.GetFileName(t);
                        tf.extension = Path.GetExtension(t);
                        t_item.Add(tf);
                    }

                }catch (UnauthorizedAccessException){

                }
                return t_item;
            }

        public string get_parent_directory(String path){
                System.IO.DirectoryInfo directoryInfo =System.IO.Directory.GetParent(path);
                return directoryInfo.FullName;
            }

        public bool get(dir_path path){
                try{
                    if(path.path==null) {
                    path.path=get_home_dir();
                    this.path=path.path;
                    }
                if(path.path.Length>=3 && path.path.Substring(path.path.Length-2)=="..") {
                    path.path=get_parent_directory(path.path.Substring(0,path.path.Length-3));
                    }
                this.path=path.path;
                files=get_files(path); 
                directories=get_directories(path.path);
                }catch {
                    return false;
                }
                return true;
            }
        }
    }

