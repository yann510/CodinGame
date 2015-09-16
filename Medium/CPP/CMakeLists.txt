cmake_minimum_required(VERSION 3.3)
project(CodinGame)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES MayanCalculation.cpp)
add_executable(CodinGame ${SOURCE_FILES})