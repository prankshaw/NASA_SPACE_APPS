from django.contrib import admin
from django.urls import path, include
from plot.views import *

urlpatterns = [
    path('test', test),
    path('disp', svdisp),
    path('accoddisp', accoddisp),
    path('popdisp', popdisp),
    path('refugg', refs)
]
