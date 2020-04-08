from PyQt5 import QtWidgets
from pyqt_wrapper_gui import Ui_MainWindow
import sys
import subprocess
from subprocess import Popen
import os
from PyQt5.QtWidgets import QFileDialog, QMessageBox 
#from PyQt5 import QStringList

    
class WrapperGUI():
    def __init__(self):
        app = QtWidgets.QApplication(sys.argv)
        self.MainWindow = QtWidgets.QMainWindow()      
        self.ui = Ui_MainWindow()                           
        self.ui.setupUi(self.MainWindow)        
        self.update_widgets()
        self.widget_actions()      
        self.MainWindow.show()
        sys.exit(app.exec_())
    
    def widget_actions(self):
        self.ui.actionExit.setStatusTip('Click to exit the application')        
        self.ui.actionExit.triggered.connect(self.close_GUI)                    # connect widget to method when triggered (clicked)
        self.ui.runButton.clicked.connect(self._run_console)
        self.ui.browseButton.clicked.connect(self._browse)

    def close_GUI(self): 
        self.MainWindow.close()       
         
    def update_widgets(self):
        self.MainWindow.setWindowTitle('PyQt5 GUI Wrapper') 

    def _run_console(self):
        print("_run_console......")
        #Popen("notepad {}".format(__file__)) # non-block
        #subprocess.run("notepad {}".format(file_summary),shell=True) # block
        exe_file        = os.path.abspath('./check_mcu_map.exe')
        map_file        = os.path.abspath('./mcu_ns.map')
        if(map_file == ''):
            print("ERROR:map_file is empty")
            #messagebox.showerror("Error", "map_file shoule not be empty!")
            #messagebox.showwarning("Warning","Warning message")
            QMessageBox.critical(self, "Error", "ap_file shoule not be empty!", QMessageBox.Yes | QMessageBox.No, QMessageBox.Yes)
        else:
            sargs = "-i " + map_file
            run_script = "{} {}".format(exe_file,sargs)
            #run_script ='.\check_mcu_map.exe -i .\mcu_ns.map'
            Popen(run_script) # non-block
            #messagebox.showinfo("Information","DONE")
            QMessageBox.information(self,"Information","Generate the map file successfully!",
									QMessageBox.Yes)
    def _browse(self):
        print("_browse....")
        #file_path_string = filedialog.askopenfilename(initialdir = "./",title = "Select file",filetypes = (("map files","*.map"),("all files","*.*")))
        #set_text(nameEntered,file_path_string)
        #scr.insert(END, file_path_string)
        dlg = QFileDialog()
        dlg.setFileMode(QFileDialog.AnyFile)
        dlg.setFilter("Map files (*.map)")
        #filenames = QStringList()
        #if dlg.exec_():
        #    filenames = dlg.selectedFiles()
        #    print("filenames:{}".format(filenames))

 
if __name__ == "__main__":
    WrapperGUI()   