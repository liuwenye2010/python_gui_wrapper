#!usr/bin/env python
#======================
# imports
#======================
from tkinter import *
import tkinter as tk
from tkinter import Menu
from tkinter import ttk
from tkinter import filedialog
from tkinter import messagebox
from tkinter import scrolledtext
import subprocess
from subprocess import Popen
import os
import sys

#======================
# functions
#======================
# Exit GUI cleanly
def _quit():
    win.quit()      # win will exist when this function is called
    win.destroy()
    exit() 

def _run_console():
    print("_run_console......")
    #Popen("notepad {}".format(__file__)) # non-block
    #subprocess.run("notepad {}".format(file_summary),shell=True) # block
    exe_file        = os.path.abspath('./check_mcu_map.exe')
    #map_file        = os.path.abspath('./mcu_ns.map')
    map_file  = nameEntered.get()
    if(map_file == ''):
        print("ERROR:map_file is empty")
        messagebox.showerror("Error", "map_file shoule not be empty!")
        #messagebox.showwarning("Warning","Warning message")
    else:
        sargs = "-i " + map_file
        run_script = "{} {}".format(exe_file,sargs)
        #run_script ='.\check_mcu_map.exe -i .\mcu_ns.map'
        Popen(run_script) # non-block
        messagebox.showinfo("Information","DONE")


def set_text(e,text):
    e.delete(0,END)
    e.insert(0,text)

    
def _browse():
    print("_brosws....")
    file_path_string = filedialog.askopenfilename(initialdir = "./",title = "Select file",filetypes = (("map files","*.map"),("all files","*.*")))
    set_text(nameEntered,file_path_string)
    #set_text(scr,file_path_string)
    scr.insert(END, file_path_string)


#======================
# procedural code
#======================
# Create instance
win = tk.Tk()   

# Add a title       
win.title("GUI Tkinter Wrapper")
win.geometry("500x500+100+100") 
# ---------------------------------------------------------------
# Creating a Menu Bar
menuBar = Menu()
win.config(menu=menuBar)

# Add menu items
fileMenu = Menu(menuBar, tearoff=0)
fileMenu.add_command(label="Open",command=_browse)
fileMenu.add_separator()
fileMenu.add_command(label="Exit", command=_quit)   # command callback
menuBar.add_cascade(label="File", menu=fileMenu)

# Add another Menu to the Menu Bar and an item
helpMenu = Menu(menuBar, tearoff=0)
helpMenu.add_command(label="About")
menuBar.add_cascade(label="Help", menu=helpMenu)
# ---------------------------------------------------------------

# Tab Control / Notebook introduced here ------------------------
tabControl = ttk.Notebook(win)          # Create Tab Control

tab1 = ttk.Frame(tabControl)            # Create a tab 
tabControl.add(tab1, text='MCU')      # Add the tab

tab2 = ttk.Frame(tabControl)            # Add a second tab
tabControl.add(tab2, text='DSP')      # Make second tab visible

tabControl.grid(column=0, row=0,sticky='W')

#tabControl.pack(expand=1, fill="both")  # Pack to make visible
# ---------------------------------------------------------------
    

# Adding a Textbox Entry widget
filename = tk.StringVar()
nameEntered = ttk.Entry(tab1, width=60, textvariable=filename)
nameEntered.grid(column=0, row=1, sticky='W')


start_browse = Button(tab1, text='Browse', command = _browse)
start_browse.grid(column=1, row=1, sticky='E')

# Using a scrolled Text control    
scrolW  = 30
scrolH  = 10
scr = scrolledtext.ScrolledText(win, width=scrolW, height=scrolH, wrap=tk.WORD)
scr.grid(column=0, row=3, sticky='WE', columnspan=3)

start_run = Button(tab1, text='Run', command = _run_console)
#start_run.pack(side=BOTTOM)
start_run.grid(column=1, row=8, padx=8, pady=4, sticky="SE")
##


#win.iconbitmap('gui.ico')

#======================
# Start GUI
#======================
win.mainloop()
