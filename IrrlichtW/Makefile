#*******************************************************
#
# Irrlicht .NET CP wrapper's native lib
# Copyright (c) 2006 Emmanuel 'DeusXL' Bossiere
# Look to the license.txt for details 
#
#*******************************************************

default: all

IRR_SDK_INCLUDE_PATH=../Irrlicht\ SDK/include
IRR_SDK_LIB=../Irrlicht\ SDK/lib/Linux
IRR_LIB_NAME=IrrlichtW

DEBUG_PATH=../bin/Debug
RELEASE_PATH=../bin/Release

COMPILER=g++
FLAGS = -I $(IRR_SDK_INCLUDE_PATH) -I/usr/include -I/usr/include/freetype2
LDFLAGS=-L/usr/X11R6/lib -lGLU -lGL -lfreetype -lXxf86vm ../Irrlicht\ SDK/lib/Linux/libIrrlicht.a

IRR_SRC= \
	animatedmesh.cpp \
	camera.cpp \
	conversion.cpp \
	customscenenodes.cpp \
	device.cpp \
	event.cpp \
	gpu.cpp \
	guielem2.cpp \
	guielem.cpp \
	guienv.cpp \
	material.cpp \
	mesh.cpp \
	meshmanipulator.cpp \
	particlescenenode.cpp \
	scenemanager.cpp \
	scenenode.cpp \
	texture.cpp \
	vertices.cpp \
	CGUITTFont.cpp \
	customfont.cpp \
	videodriver.cpp

IRR_SRC_OBJECTS=$(IRR_SRC:.cpp=.o)

.cpp.o: 
	$(COMPILER) -Os -c $(FLAGS) -o $@ $<

all: $(IRR_LIB_NAME)


$(IRR_LIB_NAME): $(IRR_SRC_OBJECTS)
	$(COMPILER) $(FLAGS) -shared $^ -o lib$@.so $(LDFLAGS)
	cp lib$(IRR_LIB_NAME).so $(DEBUG_PATH)/lib$(IRR_LIB_NAME).so
	cp lib$(IRR_LIB_NAME).so $(RELEASE_PATH)/lib$(IRR_LIB_NAME).so
	strip $(RELEASE_PATH)/lib$(IRR_LIB_NAME).so
	#*******************************************************
	#
	# Irrlicht .NET CP wrapper's native lib
	# Copyright (c) 2006 Emmanuel 'DeusXL' Bossiere
	# Look to the license.txt for details 
	#
	#*******************************************************

clean:
	rm *.o
	rm *.so
	rm $(RELEASE_PATH)/lib$(IRR_LIB_NAME).so
	rm $(DEBUG_PATH)/lib$(IRR_LIB_NAME).so