(function ( $ ) {
    $.fn.browsy = function( options ) {
        var settings = $.extend({
            path                : null,
            filters             : {},            //this includes all files. add --> filters:{ {".jpg"},{".bmp"} }
            admin               : false,         //turns all admin toggles on
            admin_hidden_folders: false,
            admin_hidden_files  : false,
            admin_filters       : false,
            controller_name     : "browsy",
            special_dirs        :[
                { name:"Desktop"      ,icon:"fas fa-desktop"      ,type:"folder" ,ext:"",special:true},
                { name:"Downloads"    ,icon:"fas fa-download"     ,type:"folder" ,ext:"",special:true},
                { name:"Documents"    ,icon:"fas fa-file"         ,type:"folder" ,ext:"",special:true},
                { name:"My Documents" ,icon:"fas fa-file"         ,type:"folder" ,ext:"",special:true},
                { name:"Pictures"     ,icon:"fas fa-camera"       ,type:"folder" ,ext:"",special:true},
                { name:"My Pictures"  ,icon:"fas fa-camera"       ,type:"folder" ,ext:"",special:true},
                { name:"Home"         ,icon:"fas fa-home"         ,type:"folder" ,ext:"",special:true},
                { name:"Projects"     ,icon:"fas fa-code-branch"  ,type:"folder" ,ext:"",special:true},
                { name:""             ,icon:"fas fa-folder"       ,type:"folder" ,ext:"",special:false},
                { name:""             ,icon:"fas fa-file"         ,type:"file"   ,ext:"",special:false}
                ],
           success_function:null,
           cancel_function :null,
           dialog_function :null,                          //for a custom message call back
           api_error       :"Bummer... Error loading the file browser data.",
           no_file_error   :"Gotta pick a file first captain."
        }, options );
        
    
      var browser= function(element,options){
            this.options          =options;                              //options for the plugin
            this.webServiceURL    =options.server;                       //the controller url for the webAPI
            this.controller_name  =options.controller_name;              //the controller name on the webapi
            this.element          =element;                              //the curent html element
            this.path             ="";                                   //the curent directory on the server
            this.directories      ={};                                   //an array of the curent directorus being viewed.
            this.files            ={};                                   //an array of the curent files being viewed
            this.active_file      =null;                                 //curent active file selected
            this.special_dirs     =options.special_dirs;                 //dir and files to be treated differently, icons or sticky
            this.build();                                                //create the html and inject it into the element.
            this.bind();                                                 // attach events to newly created HTML
            this.load(options.path,options.filters);                     //load and populate the initial data for the file browser.
        };
        //user defined functions
        browser.prototype.build=function(){
            $(this.element).html('');
            this._container=$(`<div class="container browsy">
                    <div class="row border border-left-0 border-right-0 border-top-0 border-secondary">
                       <div class="col font-weight-bold pl-3 p-1"><i class="fas fa-folder-open"></i> <span class="browsy-path"></span></div>
                    </div>
                    <div class="row browsy-row">
                       <div class="col-3 browsy-folder-container bg-light p-6 browsy-row">
                          <div class="p-3">
                             <div class="browsy-tree"></div>
                          </div>
                       </div>
                       <div class="col-7 p-0 browsy-row">
                          <div class="browsy-grid bg-light hover mb-0">
                             <div class='browsy-header row'>
                                   <div class="col-8">Name</div>
                                   <div class="col-2 text-right">Size</div>
                                   <div class="col-2 text-right">Modified</div>
                             </div>
                             <div class="browsy-file-container bg-white table-sm">
                                <div  colspan="4">No Files</div>
                             </div>
                             <div class='browsy-footer row'>
                                   <div class="col-8">Name</div>
                                   <div class="col-2 text-right">Size</div>
                                   <div class="col-2 text-right">Modified</div>
                             </div>
                          </div>
                       </div>
                       <div class="col-2 p-0 browsy-row">
                       <div class="browsy-info-container">
                          <div class="col mb-3 mt-3 block"> <button class="browsy-cancel btn-block btn btn-secondary">Cancel</button></div>
                          <div class="col mt block"> <button class="browsy-ok btn-block btn btn-primary">Open</button></div>
                          <div class="col mt-3 border border-left-0 border-right-0 border-top-0 border-secondary"></div>
                            <div class="col mt block">
                             <div class="row ml-3 mt-3 browsy-hidden-folders">
                                <div class="checkbox"><label><input class="browsy-show-hidden-folders" data-toggle="toggle" type="checkbox" data-size="small" data-onstyle="success" data-offstyle="danger"/>Sys Folders </label></div>
                             </div>
                             <div class="row ml-3 mt-3 browsy-hidden-files">
                                <div class="checkbox"><label><input class="browsy-show-hidden-files" data-toggle="toggle" type="checkbox" data-size="small" data-onstyle="success" data-offstyle="danger"/>Sys Files </label></div>
                             </div>
                             <div class="row ml-3 mt-3 browsy-filters">
                                <div class="checkbox"><label><input class="browsy-filtered" data-toggle="toggle" type="checkbox" data-size="small" data-onstyle="success" data-offstyle="danger" />Filtered</label></div>
                             </div>
                          </div>
                          <div class="col mt-3 border border-left-0 border-right-0 border-top-0 border-secondary browsy-admin-line"></div>
                       </div>
                    </div><div class="clear"></div>
                    </div>
                </div>`
                 ).appendTo(this.element);

        }

        browser.prototype.destroy=function(){
            this.unbind();
            $(this.element).html('');       //cleanup injected HTML.
            //clear variables
            this.webServiceURL    =null;
            this.controller_name  =null;
            this.element          =null;
            this.path             =null;
            this.directories      =null;
            this.files            =null;
            this.active_file      =null;
            this.special_dirs     =null;
            
        }

        browser.prototype.unbind=function(){
            $(this.element).off("change",".browsy-show-hidden-folders");
            $(this.element).off("change",".browsy-show-hidden-files");
            $(this.element).off("change",".browsy-filtered");
            $(this.element).off("click",".browsy-tree > .browsy-folder-row");
            $(this.element).off("click",".browsy-file-container > .browsy-file");
            $(this.element).off("click",".browsy-ok");
            $(this.element).off("click",".browsy-cancel");
            $(this.element).off("remove");
        }

        browser.prototype.bind=function (){
            var self=this;
            $(this.element).on("remove",$.proxy(this.destroy, this)); //listen for a kill "this plugin" event 

            if( options.admin_hidden_folders ||  options.admin) $(this.element).find(".browsy-show-hidden-folders").bootstrapToggle();
            if( options.admin_hidden_files   ||  options.admin) $(this.element).find(".browsy-show-hidden-files"  ).bootstrapToggle();
            if(!options.admin_hidden_folders && !options.admin) $(this.element).find(".browsy-hidden-folders").remove();
            if(!options.admin_hidden_files   && !options.admin) $(this.element).find(".browsy-hidden-files"  ).remove();
            if( options.admin_filters        ||  options.admin) {
                var filter=true;
                if(options.filters==null || options.filters.length==0) filter=false; 
                if(filter) {
                    $(this.element).find(".browsy-filtered").prop("checked",true);
                    $(this.element).find(".browsy-filtered"           ).bootstrapToggle();
                } else {
                    $(this.element).find(".browsy-filters"       ).remove();                                //we want filters but none are set, so dont show the buton
                }
            }

            if(!options.admin_filters        && !options.admin) $(this.element).find(".browsy-filters").remove();
            
            if(!options.admin_hidden_folders && !options.admin_hidden_files 
            && !options.admin_filters && !options.admin)  $(this.element).find(".browsy-admin-line").remove();
        
            $(this.element).on("change",".browsy-show-hidden-folders" ,function(e) {  self.refresh(); });
            $(this.element).on("change",".browsy-show-hidden-files"   ,function(e) {  self.refresh(); });
            $(this.element).on("change",".browsy-filtered"            ,function(e) {  self.refresh(); });


            $(this.element).on("click",".browsy-tree > .browsy-folder-row",function(){
                    var folder=$(this).data("folder");
                    self.load(folder,options.filters);
                });
            $(this.element).on("click",".browsy-file-container > .browsy-file",function(){
                var file=$(this).data("file");
                $(self.element).find(".browsy-file-active").removeClass("browsy-file-active");
                $(this).addClass("browsy-file-active");
                self.active_file    =file;
            });
            $(this.element).on("click",".browsy-ok",function(){
                if(null===self.active_file || null===self.path) {
                    self.message("File Dialog",self.options.no_file_error);
                    return;
                }
                if(self.options.success_function) self.options.success_function(self.path,self.active_file);
                self.close();
            });
            $(this.element).on("click",".browsy-cancel",function(){
                if(self.options.cancel_function) self.options.cancel_function();
                self.close();
            });
        }

        browser.prototype.close=function(){
            this.destroy();
        }
        browser.prototype.draw=function(){
                var file_item ={};
                var e;
                var special   =false;
                var ei    =null;
                var text  ="";
                var folder="";
                var hidden_folders    =$(this.element).find(".browsy-show-hidden-folders").is(':checked');
                var hidden_files      =$(this.element).find(".browsy-show-hidden-files")  .is(':checked');

                var d=this.directories;
                var f=this.files;

                $(this.element).find(".browsy-file-container").children().remove();                 //clear all sources
                $(this.element).find(".browsy-tree").children().remove();                           //clear all sources
                $(this.element).find(".browsy-path").text(this.path);                               //set the header path
                

                if(f===null || f===undefined || f.length===0) {                         //no files? bummer
                    $(this.element).find(".browsy-file").append("<tr><td colspan=4>No Files</td></tr>");
                }

                if(this.path!=null &&
                   this.path!="/") {                                    //do we have a up dir selection?
                    $(this.element).find(".browsy-tree").append('<div class="row browsy-folder-row"  data-folder=".."><div class="col"><i class="mr-2 fas fa-level-up-alt fa-1x" aria-hidden="true"></i><div class="file-dialog-text">..</div></div>');
                }

                //build folder and file list
                //the gammit of possiblities.. clean up later...
                if(null==d && null==f) e=[];
                if(null==d && null!=f) e=f;
                if(null==f && null!=d) e=d;
                if(null!=f && null!=d) e=d.concat(f);

                for(l=0;l<3;l++){
                    if(special===true) {
                        $(this.element).find(".browsy-tree").append('<div class="row bg-light border border-left-0 border-right-0 border-top-0 border-secondary mb-2 mt-2"></div>');
                        special=false;
                    }

                    for(i in e){
                        ei=e[i];
                        file_item=this.get_file_item_icon(ei.name,ei.type);

                        name=ei.name;                   name =name .replace(" ","&nbsp;");
                        if(l==2 && file_item.type==="file") {
                            mod =ei.modified;           mod  =mod  .replace(" ","&nbsp;");
                            size=ei.size;               size =size .replace(" ","&nbsp;");
                            file=ei.path;
                        }

                        switch(l) {
                            case 0:     if(l==0 && file_item.type==="folder" &&  file_item.special) {
                                            if(ei.hidden && !hidden_folders) continue;          //dont show hidden folders
                                            special=true;
                                            $(this.element).find(".browsy-tree").append(
                                            '<div class="row browsy-folder-row"  data-folder="'+ei.name+'">'+
                                            '<div class="col"><div class="my-auto browsy-folder-special"><i class="mr-2 '+file_item.icon+' fa-1x" aria-hidden="true"></i></div><div class="browsy-text">'+name+'</div></div>'+
                                            '');
                                        } break;
                            case 1:     if(l==1 && file_item.type==="folder" &&  !file_item.special) {
                                            if(ei.hidden && !hidden_folders) continue;          //dont show hidden folders
                                            $(this.element).find(".browsy-tree").append(
                                            '<div class="row browsy-folder-row"  data-folder="'+ei.name+'">'+
                                            '<div class="col"><div class="my-auto browsy-folder"><i class="mr-2 '+file_item.icon+' fa-1x" aria-hidden="true"></i></div><div class="browsy-text">'+name+'</div></div>'+
                                            '');
                                        } break;
                            case 2:     if(l==2 && file_item.type==="file") { //no specials for files yet
                                            if(ei.hidden && !hidden_files) continue;

                                            $(this.element).find(".browsy-file-container").append(
                                                '<div class="browsy-file row" data-file="'+ei.name+'">'+
                                                '<div class="col-sm-8"><div class="my-auto browsy-folder"><i class="mr-2 '+file_item.icon+' fa-1x" aria-hidden="true"></i></div><div class="browsy-text col-sm-8">'+name+'</div></div>'+
                                                '<div class="col-2 text-right browsy-ellipse">'+size+'</div>'+
                                                '<div class="col-2 text-right browsy-ellipse">'+mod+'</div>'+
                                            '</div>');
                                        }
                                        break;
                        } //end switch
                    }//end for in d
                }//end outside loop
            },
        
        browser.prototype.get_file_item_icon=function(name,type){

            var default_unk ={ name:""            ,icon:"fas fa-file"         ,type:"file"   ,ext:"",special:false}
            //to lower works for windows, for linux its wrong... but right now we will let it slide..
            for(i in this.special_dirs) {
                if(this.special_dirs[i].type===type &&
                   this.special_dirs[i].name.toLowerCase()===name.toLowerCase()) {
                    return this.special_dirs[i];
                }
            }
            for(i in this.special_dirs) {
                if(this.special_dirs[i].type==type &&
                   this.special_dirs[i].name=="") {
                    return this.special_dirs[i];
                }
            }
            return default_unk;     //this is the default icon...?
        },

        browser.prototype.refresh=function(){
            this.load(this.path,options.filters,true);
            this.draw();
        },

        browser.prototype.load=function(file_path,filters,absolute=false) {
            if(!absolute && file_path!== null & file_path!== undefined) {
                if(this.path==="/") {
                    file_path=this.path+file_path;
                } else {
                    file_path=this.path+"/"+file_path;
                }
            }
            var self=this;
            var filter=true;
            if(options.admin || options.admin_filters) {
                filter=$(this.element).find(".browsy-filtered").is(':checked');
            } else {
                if(options.filters==null || options.filters.length==0) filter=false; else filter=true;
            }
            
            if(!filter) filters=null;
            
            this.call_server("get_dir",function(jr) {
                self.active_file=null
                self.path       =jr.path;
                self.files      =jr.files;
                self.directories=jr.directories;
                self.draw(self);
            }
            ,function(jr){ 
                self.message("File Error",self.options.api_error); 
             },{path:file_path,filters:filters});
        }//end function
        browser.prototype.message=function(title,message) {
            if(this.options.dialog_function) this.options.dialog_function(title,message);
            else                             alert(message);

        }

        browser.prototype.call_server =function(method,success_func,err_func,da){
            if(da!==undefined && da!==null)  {
                this.data_object=da;
            } else this.data_object={};
            $.ajax({type: "POST", url: this.webServiceURL + "/"+ this.controller_name+"/" + method, data:JSON.stringify(this.data_object),
                    success: function(results){ if(success_func){success_func(results);}}, 
                    error: function(results){   if(err_func){ err_func(results); } else err(results); }, 
                    contentType: "application/json; charset=utf-8",
                    dataType: "json" });
        }
    
        browser.prototype.obj_to_string=function(obj){
            return  JSON.stringify(obj);                                                    //needs everything to be an object... {} not []
        }
        //name=the matched name
        //icon is a font awesome 5 icon
        //type =folder or file
        //ext= extension
        //sticky sorts that item first
        browser.prototype.add_special_type=function(_name,_icon,_type,_ext,_sticky){
            if(null==this.special_dirs) this.special_dirs=[];
            this.special_dirs.push({ name:_name,icon:_icon,type:_type,ext:_ext,special:_sticky});
        }
        return this.each(function() {
            new browser(this,settings);
        });
        
    };

}( jQuery ));   