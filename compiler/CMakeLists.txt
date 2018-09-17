##################################################################
# Project and CMake settings
##################################################################
cmake_minimum_required(VERSION 3.0.2)
project(Volt)
set(CMAKE_CXX_STANDARD 17)

if (NOT CMAKE_BUILD_TYPE)
	set(CMAKE_BUILD_TYPE Release)
endif ()

##################################################################
# Configuration types
##################################################################
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELWITHDEBINFO "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}")

##################################################################
# Environment variables
##################################################################

# get the version number from version.id
file(STRINGS version.id VERSION_RAW)
string(REPLACE "version:" "" VERSION ${VERSION_RAW})

# make a list
string(REPLACE "." ";" VERSION_LIST ${VERSION})

# filter version numbers
list(GET VERSION_LIST 0 VERSION_MAJOR)
list(GET VERSION_LIST 1 VERSION_MINOR)
list(GET VERSION_LIST 2 VERSION_BUILD)
list(GET VERSION_LIST 3 VERSION_REVISION)

# define it for the source files
add_definitions(-DVERSION_MAJOR=${VERSION_MAJOR}
				-DVERSION_MINOR=${VERSION_MINOR}
				-DVERSION_BUILD=${VERSION_BUILD}
				-DVERSION_REVISION=${VERSION_REVISION})

##################################################################
# using Conan for dependencies
##################################################################

# init conan
if (${CMAKE_BUILD_TYPE} STREQUAL "Debug")
	include(conan/debug/conanbuildinfo.cmake)
else ()
	include(conan/release/conanbuildinfo.cmake)
endif ()

conan_basic_setup(TARGET)

##################################################################
# UNIX / BSD specific
##################################################################
if (UNIX OR APPLE)	 
	add_definitions( -std=c++17)
	link_directories(/usr/local/lib)
	include_directories(/usr/local/include ${CMAKE_CURRENT_SOURCE_DIR}/src)

##################################################################
# MSVC specific
##################################################################
elseif (MSVC)
	add_definitions(/MP)
endif ()

##################################################################
# Includes
##################################################################
include_directories(${CMAKE_CURRENT_SOURCE_DIR}/src)

##################################################################
# Source files
##################################################################
file(GLOB SOURCE_FILES src/*.*pp)

##################################################################
# Executable
##################################################################
add_executable(Volt ${SOURCE_FILES})

##################################################################
# Libraries
##################################################################
target_link_libraries(Volt ${CONAN_LIBS})

##################################################################
# Naming
##################################################################
set_target_properties(Volt PROPERTIES DEBUG_POSTFIX -d)
