cmake_minimum_required(VERSION 3.7)
project(tinyfiledialogs)

if(APPLE)
  SET(CMAKE_C_FLAGS "-arch i386 -arch x86_64 ${CMAKE_C_FLAGS}")
endif(APPLE)

add_library(tinyfiledialogs MODULE tinyfiledialogs/tinyfiledialogs.c)
set_target_properties(tinyfiledialogs PROPERTIES BUNDLE TRUE)
