# SpatialAnalysis
A tool to capture the positions of classroom participants

Overview
This app is designed to capture the positions and movements of people from  video-recorded files. The app featured a video transform of skewed camera images into flat position on x-y coordination. Also, the app supports position captures by clicking video screen with drawing object function which marks important locations of the observation. A multi-treading function enables plays multiple videos simultaneously to watch and find exact location of people and objects on the video screen. The captured positions and movements data will be stored in a file formatted as comma-seperated CSV file. 


1. Set observation parameters
A window dialog shows to set the observation context including observation interval, N of participants, and the size of classroom. 


2. Open video file
Load a video file wants to capture positions


3. Set Homography
Click the Homography button and arrange the four points of observation area. In turn, the four points will be used in the Homography function which convert the skewed locations into flat x-y coordination.

4. Start observation
Use << and >> button to travel the time point want to capture positions. Since started the observation, the the mouse clicks on the video screen will be captured as position data.  
