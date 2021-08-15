# Let's Meet

Simple Web API project showcasing a multiple calendar comparison method returning free time intervals in which a meeting of required duration can be held.

## Description

calendar/compare endpoint accepts JSON formatted like the following example:
```
{
    "meetingDuration": "00:30",
    "calendars": [
        {
            "workingHours": 
            {
                "start": "09:00",
                "end": "19:55"
            },
            "plannedMeetings": [
                {
                    "start": "09:00",
                    "end": "10:30"
                },
	            {
                    "start": "12:00",
                    "end": "13:00"
                },
                {
                    "start": "16:00",
                    "end": "18:00"
                }
            ]
        },
        {
            "workingHours": 
            {
                "start": "10:00",
                "end": "18:30"
            },
            "plannedMeetings": [
                {
                    "start": "10:00",
                    "end": "11:30"
                },
                {
                    "start": "12:30",
                    "end": "14:30"
                },
                {
                    "start": "14:30",
                    "end": "15:00"
                },
                {
                    "start": "16:00",
                    "end": "17:00"
                }
            ]
        }
    ]
}

```

Basic validation is done on all values with proper error messages being thrown when supplied data is incorrect or missing.

Output of the example above:
```
{
    "freeIntervals": [
        {
            "start": "11:30",
            "end": "12:00"
        },
        {
            "start": "15:00",
            "end": "16:00"
        },
        {
            "start": "18:00",
            "end": "18:30"
        }
    ]
}
```

### Technologies used

- ASP.NET Core 5

#### Frameworks and utilities
- NLog
- AutoMapper

- Swagger
