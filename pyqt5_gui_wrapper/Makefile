.PHONY: all clean gui

src_icon        = gui.ico
src_main        = main.py
src_gui 		= pyqt_wrapper_gui.ui 
target-output	= main.exe
target-gui      = pyqt_wrapper_gui.py

target_all      = $(target-output) $(target-gui)


all:$(target_all)
gui:$(target-gui)

$(target-gui):$(src_gui)
	pyuic5 -x -o $@ $<
$(target-output):$(src_main) $(target-gui) 
	pyinstaller -i $(src_icon) -w $^ 
	pyinstaller -i $(src_icon) -wF $^ 

	
clean:
	rm -rf $(target_all)
	rm -rf build
	rm -rf __pycache__
	rm -rf dist
	rm -rf main.spec