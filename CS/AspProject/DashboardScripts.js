function GetDashboardItemData(viewer, itemName) {
    var itemData = viewer.GetItemData(itemName),
        data = undefined,
        axisNames = itemData.GetAxisNames();
    if (axisNames.length > 1 ) {
        if (axisNames.indexOf('Row') >= 0 && axisNames.indexOf('Column') >= 0) {
            data = GetTwoDimensionsData(itemData, 'Column', 'Row', true);
        }
        if (axisNames.indexOf('Argument') >= 0 && axisNames.indexOf('Series') >= 0) {
            data = GetTwoDimensionsData(itemData, 'Series', 'Argument');
        }
        if (axisNames.indexOf('Sparkline') >= 0) {
            var index = axisNames.indexOf('Sparkline');
            axisNames.splice(index, 1);
            data = GetOneDimensionData(itemData, axisNames[0]);
        }   
    }
    else {
        data = GetOneDimensionData(itemData, axisNames[0]);
    }
    return JSON.stringify({ I: itemName, D: data });
}


function GetAxisData(itemData, name, getTotals) {
    var axis = itemData.GetAxis(name),
            rootPoint = axis.GetRootPoint(),
            points = [],
            maxLevel;

    if (getTotals) {
        maxLevel = GetPointsRecursive(points, rootPoint, 0)
        points.push(rootPoint);
    }
    else {
        maxLevel = axis.GetDimensions().length;
        if (maxLevel == 0) {
            points.push(axis.GetRootPoint());
        }
        else {
            points = axis.GetPoints();
        }
    }
    return {
        Axis: axis,
        AxisPoints: points,
        MaxLevel: maxLevel
    };
}
function GetPointsRecursive(points, point, parentLevel) {
    var currentLevel = parentLevel;
    $.each(point.GetChildren(), function (_, child) {
        var childrenLevel = GetPointsRecursive(points, child, parentLevel + 1);
        points.push(child);
        if (childrenLevel > currentLevel)
            currentLevel = childrenLevel;
    });
    return currentLevel;
}
function GetTwoDimensionsData(itemData, columnsAxisName, rowAxisName, getTotals) {
    var columnData = GetAxisData(itemData, columnsAxisName, getTotals),
        rowData = GetAxisData(itemData, rowAxisName, getTotals),
        rows = [],
        columnDimensions = columnData.Axis.GetDimensions(),
        measures = itemData.GetMeasures();            
    for (var rowIndex = 0; rowIndex < columnData.MaxLevel; rowIndex++) {
        var row = [];
        row[rowData.MaxLevel - 1] = undefined;
        $.each(columnData.AxisPoints, function (_, point) {
            if (measures.length > 1) {
                $.each(measures, function (_, measure) {
                    row.push(GetPointValue(point, rowIndex, columnDimensions));
                });
            }
            else {
                row.push(GetPointValue(point, rowIndex, columnDimensions));
            }
        });
        rows.push(row);
    }
    if (measures.length > 1) {
        var row = [];
        row[rowData.MaxLevel - 1] = undefined;
        $.each(columnData.AxisPoints, function (_, point) {
            $.each(measures, function (_, measure) {
                row.push(measure.Name);
            });
        });
        rows.push(row);
    }

    $.each(rowData.AxisPoints, function (_, rowPoint) {
        var row = [],
            rowDimensions = rowData.Axis.GetDimensions();
        for (var columnIndex = 0; columnIndex < rowData.MaxLevel; columnIndex++) {
            row.push(GetPointValue(rowPoint, columnIndex, rowDimensions));
        }
        var rowSlice = itemData.GetSlice(rowPoint);
        if (measures.length > 0) {
            $.each(columnData.AxisPoints, function (_, columnPoint) {
                var cellSlice = rowSlice.GetSlice(columnPoint);
                $.each(measures, function (_, measure) {
                    var measureValue = cellSlice.GetMeasureValue(measure.Id);
                    row.push(measureValue.GetValue());
                });
            });
        }
        else {
            $.each(columnData.AxisPoints, function (_, columnPoint) {
                row.push();
            });
        }
        rows.push(row);
    });
    return rows;
}
function GetPointValue(point, level, dimensions) {
    var dimensionsLength = point.GetDimensions().length;
    if ((dimensionsLength == 0 && level == 0) || dimensionsLength == level + 1) {
        return point.GetDisplayText();
    }
    else {
        if (dimensionsLength > level) {
            return point.GetDimensionValue(dimensions[level].Id).GetDisplayText();
        }
        else {
            return '';
        }
    }
}

function GetOneDimensionData(itemData, axisName) {
    var axis = itemData.GetAxis(axisName),
        dimensions = axis.GetDimensions(),
        measures = itemData.GetMeasures(),
        rows = [],
        headers = [];

    $.each(dimensions, function (_, dimension) {
        headers.push(dimension.Name);
    });
    $.each(measures, function (_, measure) {
        headers.push(measure.Name);
    });
    rows.push(headers);
    $.each(axis.GetPoints(), function (_, axisPoint) {
        var row = [];
        $.each(dimensions, function (_, dimension) {
            var dimensionValue = axisPoint.GetDimensionValue(dimension.Id);
            row.push(dimensionValue.GetDisplayText());
        });
        var dataSlice = itemData.GetSlice(axisPoint);        
        $.each(measures, function (_, measure) {
            var measureValue = dataSlice.GetMeasureValue(measure.Id);
            row.push(measureValue.GetValue());
        });
        rows.push(row);
    });
    return rows;
}
