.PHONY: all clean 


target-output	= check_mcu_map.exe
target-win

target_all      = $(target-output) 

MSBUILDPATH:=$(subst $\\,/,$(subst $\",,$(shell cmd /c "build.cmd")))
$(info $(MSBUILDPATH))
MSBUILD:= $(MSBUILDPATH)/MSBuild.exe
$(info $(MSBUILD))


all:$(target_all)


$(target-output):check_mcu_map.py 
	pyinstaller -F $<
	cp ./dist/check_mcu_map.exe ./check_mcu_map.exe


   
clean:
	rm -rf $(target_all)