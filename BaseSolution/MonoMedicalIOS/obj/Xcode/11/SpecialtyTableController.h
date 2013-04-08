// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <UIKit/UIKit.h>
#import <MapKit/MapKit.h>
#import <Foundation/Foundation.h>
#import <CoreGraphics/CoreGraphics.h>


@interface SpecialtyTableController : UIViewController {
	UITableView *_specialtiesTable;
}

@property (nonatomic, retain) IBOutlet UITableView *specialtiesTable;

- (IBAction)SelectItem:(id)sender;

- (IBAction)CancelItem:(id)sender;

@end
