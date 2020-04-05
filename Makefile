.PHONY: all clean 


target-output	= check_mcu_map.exe

target_all      = $(target-output) 


all:$(target_all)


$(target-output):check_mcu_map.py 
	pyinstaller -F $<
	cp ./dist/check_mcu_map.exe ./check_mcu_map.exe
  
   
clean:
	rm -rf $(target_all)